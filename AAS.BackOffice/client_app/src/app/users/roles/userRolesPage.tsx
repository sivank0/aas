import { Box, Container, Divider, Grid, Paper, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { AccessPolicy } from '../../../domain/accessPolicies/accessPolicy';
import { UserRole } from '../../../domain/users/roles/userRole';
import { UserRoleBlank } from '../../../domain/users/roles/userRoleBlank';
import { UserRolesProvider } from '../../../domain/users/roles/userRolesProvider';
import { InputForm } from '../../../sharedComponents/inputs/inputForm';

type Policy = {
    blockKey: number;
    blockName: string;
    accessPolicies: PolicyItem[];
}

type PolicyItem = {
    key: number;
    name: string;
}

export const UserRolesPage = () => {
    const [userRoles, setUserRoles] = useState<UserRole[]>([]);
    const [policies, setPolicies] = useState<Policy[]>([]);

    const [userRoleBlank, setUserRoleBlank] = useState<UserRoleBlank | null>(null);

    useEffect(() => {
        async function init() {
            const details = await UserRolesProvider.getUserRoleDetails();

            const accessPoliciesDetails = details.accessPoliciesDetails;

            const blockKeys: number[] = [];

            accessPoliciesDetails.map(apd => {
                if (!blockKeys.includes(apd.blockKey))
                    blockKeys.push(apd.blockKey)
            });

            const policies: Policy[] = [];

            blockKeys.map(blockKey => {
                const blockName = accessPoliciesDetails.find(apd => apd.blockKey === blockKey)?.blockDisplayName ?? null;

                const accessPolicies: PolicyItem[] = [];

                accessPoliciesDetails.filter(apd => apd.blockKey === blockKey).map(apd => accessPolicies.push(toPolicyItem(apd.key, apd.displayName)));

                policies.push({ blockKey, blockName: blockName!, accessPolicies })
            })

            function toPolicyItem(key: number, name: string): PolicyItem {
                return { key, name };
            }

            setUserRoles(details.userRoles);
            setPolicies(policies)
        }

        init();
    }, [])

    function changeSelectedRole(roleId: string | null) {
        const selectedUserRole = userRoles.find(userRole => userRole.id === roleId) ?? null
        setUserRoleBlank(selectedUserRole !== null ? UserRoleBlank.fromUserRole(selectedUserRole) : null);
    }

    function changeAccessPolicies(accessPolicyKey: number, isActive: boolean) {
        if (userRoleBlank === null) return;

        setUserRoleBlank(blank => {
            const accessPolicy = accessPolicyKey as AccessPolicy;
            const userRoleBlank = { ...blank! };

            let accessPolicies = [...userRoleBlank.accessPolicies];

            if (!accessPolicies.includes(accessPolicy))
                accessPolicies.push(accessPolicy)
            else
                accessPolicies = accessPolicies.filter(ap => ap !== accessPolicy);

            userRoleBlank.accessPolicies = accessPolicies;
            return userRoleBlank;
        })
    }

    return (
        <Container maxWidth={false}>
            <Typography variant="h5">Роли пользователей</Typography>
            <Divider sx={{ marginY: 1 }} />
            <InputForm
                type="select"
                options={userRoles}
                size="small"
                sx={{ width: "150px" }}
                value={userRoles.find(userRole => userRole.id === userRoleBlank?.id) ?? null}
                getOptionLabel={(option) => option.name}
                isOptionEqualToValue={(first, second) => first.id === second.id}
                onChange={(userRole) => changeSelectedRole(userRole?.id ?? null)} />
            {
                userRoleBlank !== null &&
                <Grid container spacing={2} sx={{ marginTop: 3 }}>
                    {
                        policies.map(policy =>
                            <Grid item xs={6} >
                                <Paper sx={{ padding: 2 }} elevation={3}>
                                    <Typography>{policy.blockName}</Typography>
                                    <Divider sx={{ marginY: 1 }} />
                                    <Box display="flex" flexDirection="column">
                                        {
                                            policy.accessPolicies.map(ap =>
                                                <InputForm
                                                    type="checkBox"
                                                    label={ap.name}
                                                    checked={userRoleBlank.accessPolicies.includes(ap.key)}
                                                    onChange={(isActive) => changeAccessPolicies(ap.key, isActive)} />
                                            )
                                        }
                                    </Box>
                                </Paper>
                            </Grid>
                        )
                    }
                </Grid>
            }
        </Container>
    )
}