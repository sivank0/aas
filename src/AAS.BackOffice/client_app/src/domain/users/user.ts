export class User {
    constructor(
        public readonly id: string,
        public readonly firstName: string,
        public readonly middleName: string | null,
        public readonly lastName: string,
        public readonly fullName: string,
        public readonly email: string,
        public readonly phoneNumber: string
    ) {
    }
}

export function toUser(value: any): User {
    return new User(
        value.id,
        value.firstName,
        value.middleName,
        value.lastName,
        value.fullName,
        value.email,
        value.phoneNumber
    )
}

export function toUsers(values: any[]): User[] {
    return values.map(toUser);
}