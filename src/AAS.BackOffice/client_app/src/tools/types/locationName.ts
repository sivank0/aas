export enum LocationName {
    Users = 1,
    Bids = 2,
    UserRoles = 3,
    UserProfile = 4
}

export namespace LocationName {
    export function getDisplayName(locationName: LocationName): string {
        switch (locationName) {
            case LocationName.Users: return "Пользователи";
            case LocationName.Bids: return "Заявки";
            case LocationName.UserRoles: return "Роли пользователей";
            case LocationName.UserProfile: return "Профиль пользователя";
        }
    }
}