import PersonIcon from '@mui/icons-material/Person';
import { Box } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { AccessPolicy } from '../../domain/accessPolicies/accessPolicy';
import { FileArea } from '../../domain/files/enums/fileArea';
import SystemUser from '../../domain/systemUser';
import { UserRole } from '../../domain/users/roles/userRole';
import { UserBlank } from '../../domain/users/userBlank';
import { UsersProvider } from '../../domain/users/usersProvider';
import { SaveButton } from '../../sharedComponents/buttons/button';
import { InputForm } from '../../sharedComponents/inputs/inputForm';
import { AsyncDialogProps } from '../../sharedComponents/modals/async/types';
import { Modal, ModalActions, ModalBody, ModalTitle } from '../../sharedComponents/modals/modal';

interface Props {
    userId: string | null;
}

export const UserEditorModal: React.FC<AsyncDialogProps<Props, boolean>> = ({ open, handleClose, data: props }) => {
    const [userBlank, setUserBlank] = useState<UserBlank>(UserBlank.getDefault());
    const [userRoles, setUserRoles] = useState<UserRole[]>([]);

    useEffect(() => {
        async function loadUserRoles() {
            const userRoles = await UsersProvider.getUserRoles();
            setUserRoles(userRoles);
        }

        if (SystemUser.hasAccess(AccessPolicy.UserRolesRead))
            loadUserRoles()
    }, [])

    useEffect(() => {
        async function init() {
            if (props.userId === null) return setUserBlank(UserBlank.getDefault());

            const details = await UsersProvider.getUserDetailsForEditor(props.userId);

            if (details === null) return;

            setUserBlank(UserBlank.fromUser(details.user, details.userPermission?.roleId));
        }

        if (open) init();

        return () => setUserBlank(UserBlank.getDefault());
    }, [props.userId, open])

    async function saveUserBlank() {
        const result = await UsersProvider.saveUser(userBlank)

        if (!result.isSuccess) return alert(result.errors[0].message)

        alert('Изменения сохранены');
        handleClose(true);
    }

    return (
        <Modal isOpen={open} onClose={() => handleClose(false)}>
            <ModalTitle onClose={() => handleClose(false)}>
                {props.userId !== null ? "Редактирование" : "Добавление"} пользователя
            </ModalTitle>
            <ModalBody sx={{ width: 500 }}>
                <Box sx={{
                    display: 'flex',
                    gap: 1.5,
                    flexDirection: 'column'
                }}>
                    <InputForm
                        label=""
                        size={200}
                        type='image-input'
                        acceptTypes="image/jpeg,image/png,image/jpg"
                        fileArea={FileArea.User}
                        fileBlank={userBlank.fileBlank}
                        defaultImage={{ image: PersonIcon, size: '8rem' }}
                        alignItem={{ marginLeft: 'auto', marginRight: 'auto' }}
                        addImage={(fileBlank) => setUserBlank(userBlank => ({ ...userBlank, fileBlank }))}
                        removeImage={(fileBlank) => setUserBlank(userBlank => ({ ...userBlank, fileBlank }))} />
                    <Box sx={{
                        display: 'flex',
                        flexDirection: 'row'
                    }}>
                        <InputForm
                            sx={{ marginRight: 2 }}
                            type="text"
                            label='Имя'
                            placeholder='Введите имя'
                            value={userBlank.firstName}
                            onChange={(firstName) => setUserBlank(blank => ({ ...blank, firstName }))} />
                        <InputForm
                            type="text"
                            label='Фамилия'
                            placeholder='Введите фамилию'
                            value={userBlank.lastName}
                            onChange={(lastName) => setUserBlank(blank => ({ ...blank, lastName }))} />
                    </Box>
                    <InputForm
                        type="text"
                        label='Отчество'
                        placeholder='Введите отчество'
                        value={userBlank.middleName}
                        onChange={(middleName) => setUserBlank(blank => ({ ...blank, middleName }))} />
                    <InputForm
                        type="text"
                        label='Телефон'
                        placeholder='Введите телефон'
                        value={userBlank.phoneNumber}
                        onChange={(phoneNumber) => setUserBlank(blank => ({ ...blank, phoneNumber }))} />
                    <InputForm
                        type="text"
                        label='Email'
                        placeholder='Введите email'
                        value={userBlank.email}
                        onChange={(email) => setUserBlank(blank => ({ ...blank, email }))} />
                    {
                        userBlank.id === null &&
                        <>
                            <InputForm
                                type="password"
                                label='Пароль'
                                placeholder='Введите пароль'
                                value={userBlank.password}
                                onChange={(password) => setUserBlank(blank => ({ ...blank, password }))} />
                            <InputForm
                                type="password"
                                label='Повтор пароля'
                                placeholder='Повторите пароль'
                                value={userBlank.rePassword}
                                onChange={(rePassword) => setUserBlank(blank => ({ ...blank, rePassword }))} />
                        </>
                    }
                    {
                        (SystemUser.hasAccess(AccessPolicy.UserRolesRead) && userRoles.length !== 0) &&
                        <InputForm
                            type="select"
                            label='Выберите роль'
                            options={userRoles}
                            disableClearable
                            value={userRoles.find(role => role.id === userBlank.roleId) ?? null}
                            getOptionLabel={(option) => option.name}
                            isOptionEqualToValue={(first, second) => first.id === second.id}
                            onChange={(userRole) => setUserBlank(userBlank => ({
                                ...userBlank,
                                roleId: userRole?.id ?? null
                            }))} />
                    }
                </Box>
            </ModalBody>
            <ModalActions>
                <SaveButton
                    variant="contained"
                    onClick={() => saveUserBlank()} />
            </ModalActions>
        </Modal>
    )
}