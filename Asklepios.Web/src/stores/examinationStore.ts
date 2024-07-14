import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import { type Examination, type ExaminationDto, type InputCreateExamination } from '@/models/Examinations/examination';
import { type PaginationParams } from '@/models/paginationParams';

export const useExaminationStore = defineStore('examinationsStore', () => {
  const examinations = ref<Examination[]>([]);
  const totalItems = ref(0);
  const examinationInfo = ref<ExaminationDto | null>(null);

  function addNewExamination(examination: Examination) {
    examinations.value.push(examination);
  }

  function removeExamination(examId: number): boolean {
    const idx = examinations.value.findIndex((s) => s.examId === examId);
    if (idx === -1) return false;
    examinations.value.splice(idx, 1);
    return true;
  }

  function updateExamination(updatedExamination: Examination): boolean {
    const existingExaminationIndex = examinations.value.findIndex((m) => m.examId === updatedExamination.examId);

    if (existingExaminationIndex !== -1) {
      examinations.value[existingExaminationIndex] = updatedExamination;
      return true;
    }
    return false;
  }

  async function dispatchGetExaminations(pagination: PaginationParams) {
    const { data } = await API.examinations.getPaginatedExaminations(pagination);
    examinations.value = data;
    totalItems.value = data.length;
  }

  async function dispatchCreateExamination(input: InputCreateExamination) {
    const { data } = await API.examinations.createExamination(input);
    addNewExamination(data);
  }

  async function dispatchDeleteExamination(examId: number) {
    await API.examinations.deleteExamination(examId);
    removeExamination(examId);
  }

  async function dispatchGetExamination(examId: number) {
    const { data } = await API.examinations.getExaminationInfo(examId);
    return data;
  }

  async function dispatchUpdateExamination(examId: number, input: ExaminationDto) {
    await API.examinations.updateExamination(examId, input);
    const updatedExamination = examinations.value.find((exam) => exam.examId === examId);
    if (updatedExamination) {
      Object.assign(updatedExamination, input);
    }
  }

  async function dispatchGetExaminationsByCategory(category: string, pagination: PaginationParams) {
    const { data } = await API.examinations.getExaminationsByCategory(category, pagination);
    examinations.value = data;
    totalItems.value = data.length;
  }

  return {
    examinations,
    totalItems,
    examinationInfo,
    dispatchGetExaminations,
    dispatchCreateExamination,
    dispatchDeleteExamination,
    dispatchGetExamination,
    dispatchUpdateExamination,
    dispatchGetExaminationsByCategory,
    updateExamination
  };
});
