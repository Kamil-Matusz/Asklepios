import { computed } from 'vue'
import { required, maxLength } from '@vuelidate/validators'

export const roomRules = computed(() => ({
  roomNumber: {
    required,
  },
  roomType: {
    required,
    maxLength: maxLength(100)
  },
  numberOfBeds: {
    required,
  },
  departmentId: {
    required
  }
}))
