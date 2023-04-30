import HttpClient from "../../../tools/httpClient";
import {mapToResult, Result} from "../../../tools/types/results/result";
import {AccessPolicyDetails, mapToAccessPoliciesDetails} from "../../accessPolicies/accessPolicyDetails";
import {mapToUserRoles, UserRole} from "./userRole";
import {UserRoleBlank} from "./userRoleBlank";

export class UserRolesProvider {
    public static async saveUserRole(userRoleBlank: UserRoleBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/user_roles/save", userRoleBlank);
        return mapToResult(result);
    }

    public static async getUserRoles(): Promise<UserRole[]> {
        const userRoles = await HttpClient.getJsonAsync("/user_roles/get_all");
        return mapToUserRoles(userRoles);
    }

    public static async getAccessPolicies(): Promise<AccessPolicyDetails[]> {
        const accessPoliciesDetails = await HttpClient.getJsonAsync("/user_roles/get_access_policies_details");
        return mapToAccessPoliciesDetails(accessPoliciesDetails);
    }

    public static async removeRole(userRoleId: string): Promise<Result> {
        const result = await HttpClient.getJsonAsync("/user_roles/remove", {userRoleId});
        return mapToResult(result);
    }
}