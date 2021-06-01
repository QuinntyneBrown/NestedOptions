import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Preferences } from '@api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class PreferencesService  {

  constructor(
    @Inject(baseUrl) private readonly _baseUrl: string,
    private readonly _client: HttpClient
  ) { }

  public get(): Observable<Preferences[]> {
    return this._client.get<{ preferences: Preferences[] }>(`${this._baseUrl}api/preferences`)
      .pipe(
        map(x => x.preferences)
      );
  }

  public getById(options: { preferencesId: string }): Observable<Preferences> {
    return this._client.get<{ preferences: Preferences }>(`${this._baseUrl}api/preferences/${options.preferencesId}`)
      .pipe(
        map(x => x.preferences)
      );
  }

  public remove(options: { preferences: Preferences }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/preferences/${options.preferences.preferencesId}`);
  }

  public create(options: { preferences: Preferences }): Observable<{ preferences: Preferences }> {
    return this._client.post<{ preferences: Preferences }>(`${this._baseUrl}api/preferences`, { preferences: options.preferences });
  }

  public update(options: { preferences: Preferences }): Observable<{ preferences: Preferences }> {
    return this._client.put<{ preferences: Preferences }>(`${this._baseUrl}api/preferences`, { preferences: options.preferences });
  }
}
