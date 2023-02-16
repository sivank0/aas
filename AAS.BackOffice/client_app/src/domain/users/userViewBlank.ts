import { User } from "./user"

export interface UserViewBlank {
    firstName: string | null,
    middleName: string | null,
    lastName: string | null,
    email: string | null,
    phoneNumber: string | null
}

export namespace UserViewBlank {
    export function getDefault(): UserViewBlank {
        return {
            firstName: null,
            middleName: null,
            lastName: null,
            email: null,
            phoneNumber: null
        }
    }
}