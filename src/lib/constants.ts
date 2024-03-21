export const STORAGE_KEYS = {
  AccessToken: 'access-token',
}

export const DEFAULT_ERROR = 'Что то пошло не так. Попробуйте ещё раз позже.'

export const RoleTranslates: Record<Role, string> = {
  User: 'Пользователь',
  Admin: 'Администратор',
}

export const CollectorTypeTranslates: Record<CollectorType, string> = {
  Carbonate: 'Карбонат',
  Terrigen: 'Терриген',
}

export const ProjectTypeTranslates: Record<ProjectType, string> = {
  Ground: 'Наземное',
  Shelf: 'Шельфовое',
}
