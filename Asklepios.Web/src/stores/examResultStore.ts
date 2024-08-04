import { defineStore } from 'pinia';
import { ref } from 'vue';
import { API } from '../services';
import examResultsService from '@/services/examResults';
import { ExamResultDto, ExamResultListDto } from '@/models/Examinations/examResult';
import { PatientAutocompleteDto } from '@/models/Patients/patient';
import { ExaminationAutocompleteDto } from '@/models/Examinations/examination';

export const useExamResultsStore = defineStore('examResultsStore', () => {
  const examResults = ref<ExamResultListDto[]>([]);
  const totalItems = ref(0);
  const examResultDetails = ref<ExamResultDto | null>(null);
  const patients = ref<PatientAutocompleteDto[]>([]);
  const examinations = ref<ExaminationAutocompleteDto[]>([]);

  async function dispatchGetExamResults(pageIndex: number, pageSize: number) {
    const { data } = await examResultsService.getAllExamResults(pageIndex, pageSize);
    examResults.value = data;
    totalItems.value = data.length;
  }

  async function dispatchCreateExamResult(examResult: ExamResultDto) {
    await examResultsService.createExamResult(examResult);
    await dispatchGetExamResults(1, 10);
  }

  async function dispatchDeleteExamResult(id: string) {
    await examResultsService.deleteExamResult(id);
    await dispatchGetExamResults(1, 10);
  }

  async function dispatchGetExamResult(id: string) {
    const { data } = await examResultsService.getExamResult(id);
    examResultDetails.value = data;
    return data;
  }

  async function dispatchUpdateExamResult(id: string, examResult: ExamResultDto) {
    await examResultsService.updateExamResult(id, examResult);
    await dispatchGetExamResults(1, 10);
  }

  async function dispatchGetPatientsList() {
    const { data } = await API.patients.getPatientsList();
    patients.value = data;
    return data as PatientAutocompleteDto[];
  }

  async function dispatchGetExaminationsList() {
    const { data } = await API.examinations.getExaminationList();
    examinations.value = data;
    return data as ExaminationAutocompleteDto[];
  }

  return {
    examResults,
    totalItems,
    examResultDetails,
    patients,
    examinations,
    dispatchGetPatientsList,
    dispatchGetExamResults,
    dispatchCreateExamResult,
    dispatchDeleteExamResult,
    dispatchGetExamResult,
    dispatchUpdateExamResult,
    dispatchGetExaminationsList
  };
});
