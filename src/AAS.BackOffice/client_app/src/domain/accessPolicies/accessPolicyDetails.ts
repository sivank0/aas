export class AccessPolicyDetails {
    constructor(
        public readonly key: number,
        public readonly displayName: string,
        public readonly blockKey: number,
        public readonly blockDisplayName: string
    ) {
    }
}

export function mapToAccessPolicyDetails(value: any): AccessPolicyDetails {
    return new AccessPolicyDetails(
        value.key,
        value.displayName,
        value.blockKey,
        value.blockDisplayName
    );
}

export function mapToAccessPoliciesDetails(values: any[]): AccessPolicyDetails[] {
    return values.map(mapToAccessPolicyDetails);
}