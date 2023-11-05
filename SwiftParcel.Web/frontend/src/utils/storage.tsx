
export const saveUserInfo = (userInfo: any) => {
    localStorage.setItem("userInfo", JSON.stringify(userInfo));
};

export const getUserInfo = () => {
    const userInfo = localStorage.getItem("userInfo");
    if (userInfo) {
        return JSON.parse(JSON.stringify(userInfo));
    }
    return null;
};