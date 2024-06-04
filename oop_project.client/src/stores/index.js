import { createPinia } from 'pinia';
import { useStore } from './store';
// Почитать про localForage
const pinia = createPinia();

pinia.use(({ store }) => {
  // Подписка на изменения в хранилище
  store.$subscribe((mutation, state) => {
    // Сохранение состояния в IndexedDB
    indexedDBSave(state);
  });

  // Загрузка состояния из IndexedDB при инициализации
  const savedState = indexedDBLoad();
  if (savedState) {
    store.$patch(savedState);
  }
});

function indexedDBSave(state) {
  // Функция для сохранения состояния в IndexedDB
}

function indexedDBLoad() {
  // Функция для загрузки состояния из IndexedDB
  return savedState;
}

export { pinia };
