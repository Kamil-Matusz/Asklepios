// examResultsService.ts
import httpClient from '../httpClient';
import { ExamResultDto, ExamResultListDto } from '@/models/Examinations/examResult';

const base = 'examinations-module/ExamResults';

async function getExamResult(id: string) {
  return await httpClient.get<ExamResultDto>(`${base}/${id}`);
}

async function getAllExamResults(pageIndex: number, pageSize: number) {
  return await httpClient.get<ExamResultListDto[]>(base, { params: { pageIndex, pageSize } });
}

async function createExamResult(examResult: ExamResultDto) {
  return await httpClient.post<void>(base, examResult);
}

async function updateExamResult(id: string, examResult: ExamResultDto) {
  return await httpClient.put<void>(`${base}/${id}`, examResult);
}

async function deleteExamResult(id: string) {
  return await httpClient.delete<void>(`${base}/${id}`);
}

export default {
  getExamResult,
  getAllExamResults,
  createExamResult,
  updateExamResult,
  deleteExamResult
};
