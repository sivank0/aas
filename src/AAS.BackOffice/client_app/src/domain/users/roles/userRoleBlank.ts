import {AccessPolicy} from "../../accessPolicies/accessPolicy";
import {UserRole} from "./userRole";

export interface UserRoleBlank {
    id: string | null,
    name: string | null,
    accessPolicies: AccessPolicy[]
}

export namespace UserRoleBlank {
    export function getDefault(): UserRoleBlank {
        return {
            id: null,
            name: null,
            accessPolicies: []
        }
    }

    export function fromUserRole(userRole: UserRole): UserRoleBlank {
        return {
            id: userRole.id,
            name: userRole.name,
            accessPolicies: userRole.accessPolicies
        }
    }
}