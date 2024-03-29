import { File, mapToFile } from "../files/file";

export class User {
    constructor(
        public readonly id: string,
        public readonly photo: File | null,
        public readonly firstName: string,
        public readonly middleName: string | null,
        public readonly lastName: string,
        public readonly fullName: string,
        public readonly email: string,
        public readonly phoneNumber: string
    ) {
    }
}

export function mapToUser(value: any): User {
    return new User(
        value.id,
        value.photo === null
            ? null
            : mapToFile(value.photo),
        value.firstName,
        value.middleName,
        value.lastName,
        value.fullName,
        value.email,
        value.phoneNumber
    )
}

export function mapToUsers(values: any[]): User[] {
    return values.map(mapToUser);
}