import type { UserResponse } from 'src/users/dto/user.response'

import type { TokensResponse } from './tokens.response'

export class AuthResponse {
	user: UserResponse

	tokens: TokensResponse
}
