import HttpClient from "../../tools/httpClient";
import { mapToResult, Result } from "../../tools/results/result";
import { toUser, User } from "./user";
import { UserBlank } from "./userBlank";
import { UserRegistrationBlank } from "./userRegistrationBlank";

export class UsersProvider {
    public static async saveUser(userBlank: UserBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/users/save", userBlank);
        return mapToResult(result);
    }

    public static async registerUser(UserRegistrationBlank: UserRegistrationBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/users/register_user", UserRegistrationBlank);
        return mapToResult(result);
    }

    public static async getUserById(id: string): Promise<User | null> {
        const user = await HttpClient.getJsonAsync("/users/get_by_id", { id });
        return toUser(user);
    }
}

export namespace AuthenticationProvider {
    export async function registerUser(userRegistrationBlank: UserRegistrationBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/authentication/register_user", userRegistrationBlank);
        return mapToResult(result);
    }

    export async function logIn(email: string | null, password: string | null): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/authentication/log_in", { email, password });
        return mapToResult(result);
    }

    export async function logOut() {
        await HttpClient.postJsonAsync("/authentication/log_out");
    }
};