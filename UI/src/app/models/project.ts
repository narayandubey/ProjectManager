import { User } from "./user";

export class Project {
    public ProjectId: string;
    public ProjectName: string;
    public ProjectStartDate: string;
    public ProjectEndDate: string;
    public Priority: number;
    public User: User;
    public NoOfTasks:number;
    public NoOfCompletedTasks:number;
    constructor(){
        this.User=new User();
    }
}
