import {Box, Container, Divider, Fab, Grid, Paper, Tooltip, Typography} from '@mui/material';
import React, {useEffect, useRef, useState} from 'react';
import {AccessPolicy} from '../../../domain/accessPolicies/accessPolicy';
import {UserRole} from '../../../domain/users/roles/userRole';
import {UserRoleBlank} from '../../../domain/users/roles/userRoleBlank';
import {UserRolesProvider} from '../../../domain/users/roles/userRolesProvider';
import {InputForm} from '../../../sharedComponents/inputs/inputForm';
import SaveIcon from '@mui/icons-material/Save';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import CloseIcon from '@mui/icons-material/Close';
import useDialog from '../../../hooks/useDialog';
import {ConfirmDialogModal} from '../../../sharedComponents/modals/modal';

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
    const confirmationDialog = useDialog(ConfirmDialogModal);

    const [userRoles, setUserRoles] = useState<UserRole[]>([]);
    const [policies, setPolicies] = useState<Policy[]>([]);

    const [userRoleBlank, setUserRoleBlank] = useState<UserRoleBlank | null>(null);
    const userRoleBlankRef = useRef<UserRoleBlank | null>(userRoleBlank);

    useEffect(() => {
        async function getAccessPoliciesDetails() {
            const accessPoliciesDetails = await UserRolesProvider.getAccessPolicies();

            const blockKeys: number[] = [];

            accessPoliciesDetails.map(apd => {
                if (!blockKeys.includes(apd.blockKey))
                    blockKeys.push(apd.blockKey)
            });

            const policies: Policy[] = [];

            function toPolicyItem(key: number, name: string): PolicyItem {
                return {key, name};
            }

            blockKeys.map(blockKey => {
                const blockName = accessPoliciesDetails.find(apd => apd.blockKey === blockKey)?.blockDisplayName ?? null;

                const accessPolicies: PolicyItem[] = [];

                accessPoliciesDetails.filter(apd => apd.blockKey === blockKey).map(apd => accessPolicies.push(toPolicyItem(apd.key, apd.displayName)));

                policies.push({blockKey, blockName: blockName!, accessPolicies})
            })

            setPolicies(policies);
        }

        getAccessPoliciesDetails();
        getUserRoles();
    }, [])

    async function getUserRoles() {
        const userRoles = await UserRolesProvider.getUserRoles();
        setUserRoles(userRoles);
    }

    function addUserRoleBlank() {
        const addedUserRole = UserRoleBlank.getDefault();
        setUserRoleBlank(addedUserRole);
        userRoleBlankRef.current = addedUserRole;
    }

    async function saveUserRole() {
        if (userRoleBlank === null) return;

        const result = await UserRolesProvider.saveUserRole(userRoleBlank);

        if (!result.isSuccess) return alert(result.errors[0].message);

        setUserRoleBlank(null);
        userRoleBlankRef.current = null;
        getUserRoles();
        alert("Сохранение роли произошло успешно");
    }

    function changeSelectedRole(roleId: string | null) {
        const selectedUserRole = userRoles.find(userRole => userRole.id === roleId) ?? null
        const userRoleBlank = selectedUserRole !== null ? UserRoleBlank.fromUserRole(selectedUserRole) : null
        setUserRoleBlank(userRoleBlank);
        userRoleBlankRef.current = userRoleBlank;
    }

    function changeAccessPolicies(accessPolicyKey: number) {
        if (userRoleBlank === null) return;

        setUserRoleBlank(blank => {
            const accessPolicy = accessPolicyKey as AccessPolicy;
            const userRoleBlank = {...blank!};

            let accessPolicies = [...userRoleBlank.accessPolicies];

            if (!accessPolicies.includes(accessPolicy))
                accessPolicies.push(accessPolicy)
            else
                accessPolicies = accessPolicies.filter(ap => ap !== accessPolicy);

            userRoleBlank.accessPolicies = accessPolicies;
            return userRoleBlank;
        })
    }

    async function cancelEditingRole() {
        if (JSON.stringify(userRoleBlank) === JSON.stringify(userRoleBlankRef.current)) {
            setUserRoleBlank(null);
            return userRoleBlankRef.current = null;
        }

        const isConfirmed = await confirmationDialog.show({title: "Вы действительно хотите прекратить редактировать роль?"});

        if (!isConfirmed) return;

        setUserRoleBlank(null);
        userRoleBlankRef.current = null;
    }

    async function removeUserRole() {
        if (userRoleBlank === null) return;

        const isConfirmed = await confirmationDialog.show({title: "Вы действительно хотите удалить роль?"});

        if (!isConfirmed) return;

        if (userRoleBlank.id !== null) {
            const result = await UserRolesProvider.removeRole(userRoleBlank.id);

            if (!result.isSuccess) return alert(result.errors[0].message);
        }

        setUserRoleBlank(null);
        userRoleBlankRef.current = null;
        getUserRoles();
        alert("Удаление роли произошло успешно");
    }

    return (
        <Container maxWidth={false}>
            <Typography variant="h5">Роли пользователей</Typography>
            <Divider sx={{marginTop: 2, marginBottom: 3}}/>
            {
                userRoleBlank == null
                    ?
                    <InputForm
                        type="select"
                        options={userRoles}
                        size="small"
                        label='Выберите роль'
                        sx={{width: 300}}
                        value={null}
                        getOptionLabel={(option) => option.name}
                        isOptionEqualToValue={(first, second) => first.id === second.id}
                        onChange={(userRole) => changeSelectedRole(userRole?.id ?? null)}/>
                    :
                    <InputForm
                        sx={{width: 300}}
                        type="text"
                        label='Название роли'
                        size="small"
                        placeholder='Введите название роли'
                        value={userRoleBlank.name}
                        onChange={(name) => setUserRoleBlank(blank => ({...blank!, name}))}/>
            }
            {
                userRoleBlank !== null &&
                <Grid container spacing={2} sx={{marginTop: 3}}>
                    {
                        policies.map(policy =>
                            <Grid item xs={6}>
                                <Paper sx={{padding: 2}} elevation={3}>
                                    <Typography>{policy.blockName}</Typography>
                                    <Divider sx={{marginY: 1}}/>
                                    <Box display="flex" flexDirection="column">
                                        {
                                            policy.accessPolicies.map(ap =>
                                                <InputForm
                                                    type="checkBox"
                                                    label={ap.name}
                                                    checked={userRoleBlank.accessPolicies.includes(ap.key)}
                                                    onChange={() => changeAccessPolicies(ap.key)}/>
                                            )
                                        }
                                    </Box>
                                </Paper>
                            </Grid>
                        )
                    }
                </Grid>
            }
            {
                userRoleBlank === null
                    ?
                    <Tooltip title="Добавить роль">
                        <Fab
                            color='success'
                            onClick={() => addUserRoleBlank()}
                            sx={{
                                position: "absolute",
                                right: 24,
                                bottom: 24
                            }}>
                            <AddIcon/>
                        </Fab>
                    </Tooltip>
                    :
                    <Box sx={{display: "flex", position: "absolute", right: 24, bottom: 24, gap: 3}}>
                        <Tooltip title="Сохранить роль">
                            <Fab onClick={() => saveUserRole()} color='primary'>
                                <SaveIcon/>
                            </Fab>
                        </Tooltip>
                        {
                            userRoleBlank.id !== null &&
                            <Tooltip title="Удалить роль">
                                <Fab onClick={() => removeUserRole()} color='error'>
                                    <DeleteIcon/>
                                </Fab>
                            </Tooltip>
                        }
                        <Tooltip title="Прекратить редактирование">
                            <Fab
                                onClick={() => cancelEditingRole()}
                                sx={theme => ({
                                    backgroundColor: theme.palette.grey[400],
                                    "&:hover": {
                                        backgroundColor: theme.palette.grey[500]
                                    }
                                })}>
                                <CloseIcon/>
                            </Fab>
                        </Tooltip>
                    </Box>
            }
        </Container>
    )
}