import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { User } from '@api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    @Inject(baseUrl) private readonly _baseUrl: string,
    private readonly _client: HttpClient
  ) { }

  public get(): Observable<User[]> {
    return this._client.get<{ users: User[] }>(`${this._baseUrl}api/user`)
      .pipe(
        map(x => x.users)
      );
  }

  public getById(options: { userId: string }): Observable<User> {
    return this._client.get<{ user: User }>(`${this._baseUrl}api/user/${options.userId}`)
      .pipe(
        map(x => x.user)
      );
  }

  public remove(options: { user: User }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/user/${options.user.userId}`);
  }

  public create(options: { user: User }): Observable<{ user: User }> {
    return this._client.post<{ user: User }>(`${this._baseUrl}api/user`, { user: options.user });
  }

  public update(options: { user: User }): Observable<{ user: User }> {
    return this._client.put<{ user: User }>(`${this._baseUrl}api/user`, { user: options.user });
  }
}
