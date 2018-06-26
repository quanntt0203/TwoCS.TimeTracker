const ROLE = {
    Admin: 'Admin',
    Manager: 'Manager',
    User: 'User'
};

export const getIdentity= () => {
    const identity = localStorage.getItem("identity");
    if (identity) {
        return JSON.parse(identity);
    }

    return null;
};

export const getIdentityRole = () => {
    const user = getIdentity();

    return getUserRole(user);
};

export const getUserRole = (user) => {
    if (user) {
        const role = {
            roles: user.roles,
            isAdmin: user.roles.indexOf(ROLE.Admin) >= 0 && user.behaveOfMagager == null,
            isManager: user.roles.indexOf(ROLE.Manager) >= 0 || user.behaveOfMagager != null,
            isUser: user.roles.indexOf(ROLE.User) >= 0
        };

        return role;
    }

    return null;
};
