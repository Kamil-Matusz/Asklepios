import { computed } from 'vue'
import { required, maxLength } from '@vuelidate/validators'

export const medicalStuffRules = computed(() => ({
    name: {
      required,
      maxLength: maxLength(100)
    },
    surname: {
      required,
      maxLength: maxLength(100)
    },
    privatePhoneNumber: {
      required,
      maxLength: maxLength(12)
    },
    hospitalPhoneNumber: {
      required,
      maxLength: maxLength(12)
    },
    specialization: {
      required,
      maxLength: maxLength(200)
    },
    medicalLicenseNumber: {
      required,
      maxLength: maxLength(12)
    },
    userId: {
      required,
    },
    departmentId: {
      required
    }
  }))
