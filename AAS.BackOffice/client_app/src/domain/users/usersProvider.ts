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
        const result = await HttpClient.postJsonAsync("/users/createUser", UserRegistrationBlank);
        return mapToResult(result);
    }

    public static async getUserById(id: string): Promise<User | null> {
        const user = await HttpClient.getJsonAsync("/users/get_by_id", { id });
        return toUser(user);
    }
}