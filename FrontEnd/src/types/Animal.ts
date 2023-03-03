import { IListTagsDto, ITagDto } from "./Tag";

export interface AnimalDto {
    animalOriginUrlDto: IAnimalOriginUrlDto,
    listTagsDto: IListTagsDto,
    photo: string
}

export interface AnimalGetDto {
    id:number,
    originId:number,
    photo: string,
    selectedTags: Array<number>
}

interface IAnimalOriginUrlDto {
    id: number,
    url: string,
    needRetrieveUrlInBody: boolean
}

export interface ICreateAnimalDto {
    origin: number,
    user: number,
    tags: Array<number>,
    photo: string
}

export interface IAnimalHistory {
    id: number
    userId: number,
    animalOriginId: number,
    tags: Array<ITagDto>,
    photo: string
}