import HttpClient from "../../tools/httpClient";
import { mapToResult, Result } from "../../tools/types/results/result";
import { mapToUserPermission, UserPermission } from "./permissions/userPermission";
import { mapToUserRole, mapToUserRoles, UserRole } from "./roles/userRole";
import { toUser, toUsers, User } from "./user";
import { UserBlank } from "./userBlank";
import { UserRegistrationBlank } from "./userRegistrationBlank";

export class UsersProvider {
    public static async saveUser(userBlank: UserBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/users/save", userBlank);
        return mapToResult(result);
    }

    public static async getUserById(id: string): Promise<User | null> {
        const user = await HttpClient.getJsonAsync("/users/get_by_id", { id });
        return toUser(user);
    }

    public static async getUserDetailsForEditor(userId: string): Promise<{ user: User, userRoles: UserRole[], userPermission: UserPermission | null } | null> {
        const details = await HttpClient.getJsonAsync("/users/get_details_for_editor", { userId });

        if (details === null) return null;

        return {
            user: toUser(details.user),
            userRoles: mapToUserRoles(details.userRoles),
            userPermission: details.userPermission === null
                ? null
                : mapToUserPermission(details.userPermission)
        };
    }



    public static async getUsers(): Promise<User[]> {
        const users = await HttpClient.getJsonAsync("users/get_all");
        return toUsers(users);
    }

    public static async changeUserPassword(userId: string, password: string | null, rePassword: string | null): Promise<Result> {
        const result = await HttpClient.getJsonAsync("users/change_password", { userId, password, rePassword });
        return mapToResult(result);
    }

    public static async removeUser(userId: string): Promise<Result> {
        const result = await HttpClient.getJsonAsync("users/remove", { userId });
        return mapToResult(result);
    }

    //UserRoles
    public static async getUserRole(userId: string): Promise<UserRole | null> {
        const userRole = await HttpClient.getJsonAsync("users/get_role_by_user_id", { userId });

        if (userRole === null) return null;

        return mapToUserRole(userRole);
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