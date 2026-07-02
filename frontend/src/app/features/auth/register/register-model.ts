export interface RegisterRequest{
    email: string;
    password: string;
    phone: string;
    
}
export interface RegisterResponse{
    userId: string;
    email: string;
    createdAt: Date;
}