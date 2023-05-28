import {AccessPolicy} from "./accessPolicies/accessPolicy";
import {File} from "./files/file";

export class SystemUser {
    constructor(
        public readonly id: string,
        public readonly photo: File | null,
        public readonly email: string,
        public readonly fullName: string,
        public readonly hasFullAccess: boolean,
        public readonly availableAccessPolicies: AccessPolicy[]
    ) {
    }

    public static loadSystemUser(): SystemUser | null {
        const systemUser = (window as any).systemUser;

        if (!systemUser) return null;

        return new SystemUser(
            systemUser.id,
            systemUser.photo,
            systemUser.email,
            systemUser.fullName,
            systemUser.hasFullAccess,
            systemUser.availableAccessPolicies
        )
    }

    public hasAccess = (accessPolicy: AccessPolicy): boolean => {
        return this.availableAccessPolicies.includes(accessPolicy);
    }
}

const systemUser: SystemUser | null = SystemUser.loadSystemUser();

export default systemUser!;