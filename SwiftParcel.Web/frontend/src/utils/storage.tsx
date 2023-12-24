
export const saveUserInfo = (userInfo: any) => {
    localStorage.setItem("userInfo", JSON.stringify(userInfo));
    console.log("User info in saveuserInfo",  JSON.stringify(userInfo));
};

export const getUserInfo = () => {
    const userInfo = localStorage.getItem("userInfo");
    return userInfo ? JSON.parse(userInfo) : null;
};