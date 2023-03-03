import http from "../http-common";
import { AnimalDto, AnimalGetDto, ICreateAnimalDto } from "../types/Animal";

export const getNewAnimalData = async (): Promise<AnimalDto> => {
    const response = await http.get("Animal/");
    return response.data;
}

export const getAnimalData = async (id: number): Promise<AnimalGetDto> => {
    const response = await http.get("Animal/" + id);
    return response.data;
}

export const createAnimal = async (
    origin: number,
    user: number,
    tags: Array<number>,
    photo: string
): Promise<number> => {
    const data: ICreateAnimalDto = { origin, user, tags, photo };
    const response = await http.post("Animal/", data);
    return response.data;
}

export const updateAnimal = async (
    id: number,
    user: number,
    tags: Array<number>
): Promise<number> => {
    const data = { id, user, tags };
    const response = await http.put("Animal/" + id, data);
    return response.data;
}