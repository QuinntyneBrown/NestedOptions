import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { User, UserService } from '@api';
import { EMPTY, merge, Observable, of, Subject } from 'rxjs';
import { map, startWith, switchMap, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  private readonly _defualts:any = {
    isAdmin: false,
    preferences: {
      allowSocialSignIn: false,
      allowMultipleLanguages: false
    }
  };

  private readonly _destroyed$: Subject<void> = new Subject();

  private readonly _refresh$: Subject<void> = new Subject();

  private readonly _user$: Subject<User|null> = new Subject();

  public vm$: Observable<{ users: User[]}> = merge(this._refresh$, this._user$).pipe(
    startWith(true),
    switchMap(x => {
      if(typeof x != "boolean" && x !== undefined) {

        if(x == null) {
          this.form.reset(this._defualts);
          return EMPTY;
        }

        this.form.patchValue(x as User, { emitEvent: false });

        return EMPTY;
      }

      return of(x);
    }),
    switchMap(x => this._userService.get()),
    map(users => ({ users }))
  );

  public readonly form: FormGroup = new FormGroup({
    userId: new FormControl(null,[]),
    username: new FormControl(null,[]),
    isAdmin: new FormControl(false,[]),
    preferences: new FormGroup({
      allowSocialSignIn: new FormControl(false,[]),
      allowMultipleLanguages: new FormControl(false,[])
    })
  })

  constructor(
    private readonly _userService: UserService
  ) { }

  public save() {
    var user = this.form.value as User;

    const obs$ = user.userId ?  this._userService.update({ user }) :this._userService.create({ user });

    obs$
    .pipe(
      takeUntil(this._destroyed$),
      tap(x => {
        this.form.reset(),
        this._refresh$.next()
      }),
    )
    .subscribe()

  }

  public cancel() {
    this._user$.next(null);
  }

  public edit(user:User) {
    this._user$.next(user);
  }

  public delete(user:User) {
    this._userService.remove({ user })
    .pipe(
      takeUntil(this._destroyed$),
      tap(x => {
        this._refresh$.next()
      }),
    )
    .subscribe()
  }
}
