import { createI18n } from 'vue-i18n';

const messages = {
  en: {
    Doctor: "Doctor",
    Nurse: "Nurse",
    roles: {
      doctor: "Doctor",
      nurse: "Nurse",
      admin: "Administrator",
      user: "User",
    }
  },
  pl: {
    Doctor: "Lekarz",
    Nurse: "Pielęgniarka",
    roles: {
      doctor: "Lekarz",
      nurse: "Pielęgniarka",
      admin: "Administrator",
      user: "Użytkownik",
    }
  }
};

const i18n = createI18n({
  locale: 'pl',
  fallbackLocale: 'en',
  messages,
});

export default i18n;
