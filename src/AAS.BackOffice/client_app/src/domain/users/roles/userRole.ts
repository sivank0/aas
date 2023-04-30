import {AccessPolicy} from "../../accessPolicies/accessPolicy";

export class UserRole {
    constructor(
        public readonly id: string,
        public readonly name: string,
        public readonly accessPolicies: AccessPolicy[]
    ) {
    }
}

export function mapToUserRole(value: any): UserRole {
    return new UserRole(value.id, value.name, value.accessPolicies);
}

export function mapToUserRoles(values: any[]): UserRole[] {
    return values.map(mapToUserRole);
}