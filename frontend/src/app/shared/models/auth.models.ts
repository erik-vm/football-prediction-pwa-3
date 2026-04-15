export interface User { id: string; username: string; email: string; role: string; createdAt: string; }
export interface AuthResponse { accessToken: string; refreshToken: string; user: User; }
export interface LoginRequest { email: string; password: string; }
export interface RegisterRequest { username: string; email: string; password: string; }
