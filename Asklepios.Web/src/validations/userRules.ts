
import { computed } from 'vue'
import { required, email, maxLength } from '@vuelidate/validators'

export const userRules = computed(() => ({
  email: {
    required,
    email,
    maxLength: maxLength(100)
  },
  password: {
    required,
    maxLength: maxLength(200)
  },
  role: {
    required,
  },
  isActive: {
    required
  }
}))
