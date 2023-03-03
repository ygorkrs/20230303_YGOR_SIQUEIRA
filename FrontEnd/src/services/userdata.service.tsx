import http from "../http-common";
import { IUserHistory } from "../types/UserHistory";

export const getHistoryUser = async (userId: number): Promise<IUserHistory> => {
    const response = await http.get("User/" + userId);
    return response.data;
}

export const createUser = async (userIdentification: string): Promise<number> => {
    const tagData = { userIdentification };
    const response = await http.post("User/", tagData);
    return response.data;
}