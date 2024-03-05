import type { UserResponse } from 'src/users/dto'

export class AuthResponse {
	token: string

	user: UserResponse
}
