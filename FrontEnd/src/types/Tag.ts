export interface IListTagsDto {
    listTagsAvailable: Array<ITagDto>
}

export interface ITagDto {
    id: number,
    tagName: string
}