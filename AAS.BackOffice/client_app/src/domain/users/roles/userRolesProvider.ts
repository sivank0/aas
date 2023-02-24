import HttpClient from "../../../tools/httpClient";
import { mapToResult, Result } from "../../../tools/results/result";
import { AccessPolicyDetails, mapToAccessPoliciesDetails } from "../../accessPolicies/accessPolicyDetails";
import { mapToUserRoles, UserRole } from "./userRole";
import { UserRoleBlank } from "./userRoleBlank";

export class UserRolesProvider {
    public static async saveUserRole(userRoleBlank: UserRoleBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/user_roles/save", userRoleBlank);
        return mapToResult(result);
    }

    public static async getUserRoleDetails(): Promise<{ userRoles: UserRole[], accessPoliciesDetails: AccessPolicyDetails[] }> {
        const details = await HttpClient.getJsonAsync("/user_roles/get_details");

        return {
            userRoles: mapToUserRoles(details.userRoles),
            accessPoliciesDetails: mapToAccessPoliciesDetails(details.accessPoliciesDetails)
        };
    }
}