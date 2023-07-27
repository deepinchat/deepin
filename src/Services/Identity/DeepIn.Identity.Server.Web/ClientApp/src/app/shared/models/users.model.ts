export interface UserViewModel {
    id: string;
    email: string;
    phoneNumber: string;
    userName: string;
    createdAt: string;
    profile: UserProfileViewModel;
}

export interface UserProfileViewModel {
    nickName: string;
    picture: string;
    birthDate: string;
    gender: string;
    zoneInfo: string;
    bio: string;
}