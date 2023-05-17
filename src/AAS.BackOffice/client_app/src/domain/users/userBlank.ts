import { FileBlank } from "../files/fileBlank"
import { User } from "./user"

export interface UserBlank {
    id: string | null,
    firstName: string | null,
    middleName: string | null,
    lastName: string | null,
    email: string | null,
    fileBlank: FileBlank | null,
    phoneNumber: string | null,
    roleId: string | null,
    password: string | null,
    rePassword: string | null
}

export namespace UserBlank {
    export function getDefault(): UserBlank {
        return {
            id: null,
            firstName: null,
            middleName: null,
            lastName: null,
            email: null,
            fileBlank: null,
            phoneNumber: null,
            roleId: null,
            password: null,
            rePassword: null
        }
    }

    export function fromUser(user: User, roleId: string | null = null): UserBlank {
        return {
            id: user.id,
            firstName: user.firstName,
            middleName: user.middleName,
            lastName: user.lastName,
            email: user.email,
            fileBlank: user.photo === null 
                ? null 
                : FileBlank.fromFile(user.photo),
            phoneNumber: user.phoneNumber,
            roleId: roleId,
            password: null,
            rePassword: null
        }
    }
}