<template>
  <GridPanel>
    <div class="sticky z-10 flex flex-col w-full pt-5 bg-primary-900">
      <HeaderWrapper>
        <MiddleHeader />
      </HeaderWrapper>
    </div>

    <slot />
  </GridPanel>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import GridPanel from '@/ui/GridPanel/GridPanel.vue';
import HeaderWrapper from '@/ui/Header/HeaderWrapper.vue';
import MiddleHeader from '@/ui/Header/MiddleHeader.vue';

export default defineComponent({
  components: {
    GridPanel,
    HeaderWrapper,
    MiddleHeader,
  },
});
</script>

<style scoped></style>
