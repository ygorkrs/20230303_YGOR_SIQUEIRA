import http from "../http-common";
import { IListTagsDto } from "../types/Tag";

export const getTagsAvailable = async (): Promise<IListTagsDto> => {
    const response = await http.get("Tag/");
    return response.data;
}

export const createTag = async (tagName: string, userId: number): Promise<number> => {
    const tagData = { tagName, userId };
    const response = await http.post("Tag/", tagData);
    return response.data;
}