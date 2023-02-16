import { User } from "./user"

export interface UserRegistrationBlank {
    id: string | null,
    firstName: string | null,
    middleName: string | null,
    lastName: string | null,
    email: string | null,
    password: string | null,
    rePassword: string | null,
    phoneNumber: string | null
}

export namespace UserRegistrationBlank {
    export function getDefault(): UserRegistrationBlank {
        return {
            id: null,
            firstName: null,
            middleName: null,
            lastName: null,
            email: null,
            password: null,
            rePassword: null,
            phoneNumber: null
        }
    }
}