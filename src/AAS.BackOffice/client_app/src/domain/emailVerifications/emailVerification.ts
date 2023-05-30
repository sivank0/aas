export class EmailVerification {
    constructor(
        public readonly userId: string,
        public readonly token: string,
        public readonly isVerified: boolean
    ) { }
}

export function mapToEmailVerification(value: any): EmailVerification {
    return new EmailVerification(value.userId, value.token, value.isVerified);
}