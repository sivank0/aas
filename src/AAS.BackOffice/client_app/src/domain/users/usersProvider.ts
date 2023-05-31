import HttpClient from "../../tools/httpClient";
import { mapToResult, Result } from "../../tools/types/results/result";
import { mapToUserPermission, UserPermission } from "./permissions/userPermission";
import { mapToUserRole, mapToUserRoles, UserRole } from "./roles/userRole";
import { mapToUser, mapToUsers, User } from "./user";
import { UserBlank } from "./userBlank";
import { UserRegistrationBlank } from "./userRegistrationBlank";

export namespace UsersProvider {
    export async function saveUser(userBlank: UserBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/users/save", userBlank);
        return mapToResult(result);
    }

    export async function getUserById(id: string): Promise<User | null> {
        const user = await HttpClient.getJsonAsync("/users/get_by_id", { id });
        return mapToUser(user);
    }

    export async function getUserDetailsForEditor(userId: string): Promise<{
        user: User,
        userPermission: UserPermission | null
    } | null> {
        const details = await HttpClient.getJsonAsync("/users/get_details_for_editor", { userId });

        if (details === null) return null;

        return {
            user: mapToUser(details.user),
            userPermission: details.userPermission === null
                ? null
                : mapToUserPermission(details.userPermission)
        };
    }

    export async function getUsers(): Promise<User[]> {
        const users = await HttpClient.getJsonAsync("users/get_all");
        return mapToUsers(users);
    }

    export async function changeUserPassword(userId: string, password: string | null, rePassword: string | null): Promise<Result> {
        const result = await HttpClient.postJsonAsync("users/change_password", { userId, password, rePassword });
        return mapToResult(result);
    }

    export async function removeUser(userId: string): Promise<Result> {
        const result = await HttpClient.getJsonAsync("users/remove", { userId });
        return mapToResult(result);
    }

    //UserRoles
    export async function getUserRole(userId: string): Promise<UserRole | null> {
        const userRole = await HttpClient.getJsonAsync("users/get_role_by_user_id", { userId });
        return userRole === null
            ? null
            : mapToUserRole(userRole);
    }

    export async function getUserRoles(): Promise<UserRole[]> {
        const userRoles = await HttpClient.getJsonAsync("users/get_all_roles");
        return mapToUserRoles(userRoles);
    }
}

export namespace UserProfileProvider {
    export async function saveUser(userBlank: UserBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/user_profile/save", userBlank);
        return mapToResult(result);
    }

    export async function getUserProfileById(userId: string): Promise<User | null> {
        const user = await HttpClient.getJsonAsync("/user_profile/get_by_id", { userId });
        return mapToUser(user);
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
}