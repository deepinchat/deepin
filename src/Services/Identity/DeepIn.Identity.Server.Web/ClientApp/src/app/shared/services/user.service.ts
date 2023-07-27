import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserProfileViewModel, UserViewModel } from '../models/users.model';

const USERS_URL = environment.baseUrl + '/api/users';
@Injectable()
export class UserService {

  constructor(
    private httpClient: HttpClient
  ) { }

  get() {
    return this.httpClient.get<UserViewModel>(USERS_URL);
  }

  updateProfile(model: UserProfileViewModel) {
    return this.httpClient.post<void>(`${USERS_URL}/profile`, model);
  }
}
