import { User } from "./user";

export class Task {
    public Start_Date: string;
    public End_Date: string;
    public Task_Name: string;
    public Project_ID: number;
    public TaskId: number;
    public Priority: number;
    public Parent_ID: number;
    public Status: number;
    public User: User;
    public ParentTaskName: string;

    constructor() {
        this.User = new User();
    }
}
