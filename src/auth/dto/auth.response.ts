import type { UserResponse } from 'src/users/dto'

import type { TokensResponse } from './tokens.response'

export class AuthResponse {
	user: UserResponse

	tokens: TokensResponse
}
