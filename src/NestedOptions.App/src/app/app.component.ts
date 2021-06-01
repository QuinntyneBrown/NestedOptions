import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { User, UserService } from '@api';
import { Observable, Subject } from 'rxjs';
import { map, startWith, switchMap, tap } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  private readonly _refresh: Subject<void> = new Subject();

  public vm$: Observable<{ users: User[]}> = this._refresh.pipe(
    startWith(true),
    switchMap(x => this._userService.get()),
    map(users => ({ users }))
  );

  public readonly form: FormGroup = new FormGroup({
    username: new FormControl(null,[]),
    isAdmin: new FormControl(false,[]),
    preferences: new FormGroup({
      allowSocialSignIn: new FormControl(false,[]),
      allowMultipleLanguages: new FormControl(false,[])
    })
  })

  constructor(
    private readonly _userService: UserService
  ) {

  }

  public save() {
    var user = this.form.value;

    this._userService.create({ user })
    .pipe(
      tap(x => {
        this.form.reset(),
        this._refresh.next()
      }),
    )
    .subscribe()
  }
}
