import HttpClient from "../../tools/httpClient";
import { Result, mapToResult } from "../../tools/types/results/result";
import { User, mapToUser } from "../users/user";
import { EmailVerification, mapToEmailVerification } from "./emailVerification";

export namespace EmailVerificationsProvider {
    export async function resendEmailVerificationMessage(userEmail: string | null): Promise<Result> {
        const result = await HttpClient.getJsonAsync("/email_verificaton/resend_email_verification_message", { userEmail });
        return mapToResult(result);
    }

    export async function getUserEmailVerification(userEmailVerificationToken: string): Promise<{ user: User, emailVerificaton: EmailVerification } | null> {
        const details = await HttpClient.getJsonAsync("/email_verifications/get_user_email_verification", { userEmailVerificationToken });

        if (details === null) return null;

        return {
            user: mapToUser(details.user),
            emailVerificaton: mapToEmailVerification(details.emailVerification)
        };
    }

    export async function confirmEmail(userEmailVerificationToken: string | null): Promise<Result> {
        const result = await HttpClient.getJsonAsync("/email_verifications/confirm_email", { userEmailVerificationToken });
        return mapToResult(result);
    }
}