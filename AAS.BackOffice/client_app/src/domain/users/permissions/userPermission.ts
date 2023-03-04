export class UserPermission {
    constructor(
        public readonly userId: string,
        public readonly roleId: string
    ) { }
}

export function mapToUserPermission(value: any): UserPermission {
    return new UserPermission(value.userId, value.roleId);
}