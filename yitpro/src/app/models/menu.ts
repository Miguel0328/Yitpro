export interface IMenu{
    Id: number, 
    IdParent?: number, 
    Description: string,
    Controller?: string,
    Action?: string, 
    Order: number,  
    Level: number, 
    Icon: string,
    Menus?: IMenu[]
}