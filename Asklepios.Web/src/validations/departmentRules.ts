import { computed } from 'vue'
import { required, maxLength } from '@vuelidate/validators'

export const departmentRules = computed(() => ({
  departmentName: {
    required,
    maxLength: maxLength(100)
  },
  numberOfBeds: {
    required,
  },
  actualNumberOfPatients: {
    required,
  }
}))
