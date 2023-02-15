import { User } from "./user"

export interface UserAuthorizationBlank {
    email: string | null,
    password: string | null
}

export namespace UserAuthorizationBlank {
    export function getDefault(): UserAuthorizationBlank {
        return {
            email: null,
            password: null
        }
    }
}