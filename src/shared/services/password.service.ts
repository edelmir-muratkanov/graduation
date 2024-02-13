import { Injectable } from '@nestjs/common'
import * as bcrypt from 'bcrypt'

@Injectable()
export class PasswordService {
	async hashPassword(password: string) {
		const salt = await bcrypt.genSalt(10)
		return bcrypt.hash(password, salt)
	}

	comparePasswords(password: string, hash: string) {
		return bcrypt.compare(password, hash)
	}
}
