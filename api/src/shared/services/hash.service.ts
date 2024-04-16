import { Injectable } from '@nestjs/common'
import bcrypt from 'bcrypt'

@Injectable()
export class HashService {
	async hash(data: string) {
		const salt = await bcrypt.genSalt(10)
		return bcrypt.hash(data, salt)
	}

	compareData(data: string, hashed: string) {
		return bcrypt.compare(data, hashed)
	}
}
