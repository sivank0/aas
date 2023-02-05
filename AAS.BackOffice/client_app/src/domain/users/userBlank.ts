import { User } from "./user"

export interface UserBlank {
    id: string | null,
    firstName: string | null,
    middleName: string | null,
    lastName: string | null,
    email: string | null,
    phoneNumber: string | null
}

export namespace UserBlank {
    export function getDefault(): UserBlank {
        return {
            id: null,
            firstName: null,
            middleName: null,
            lastName: null,
            email: null,
            phoneNumber: null
        }
    }

    export function fromUser(user: User): UserBlank {
        return {
            id: user.id,
            firstName: user.firstName,
            middleName: user.middleName,
            lastName: user.lastName,
            email: user.email,
            phoneNumber: user.phoneNumber
        }
    }
}