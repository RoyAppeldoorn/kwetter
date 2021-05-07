<template>
  <FixedGridPanel>
    <HeaderWrapper>
      <LeftHeader />
    </HeaderWrapper>
    <slot />
  </FixedGridPanel>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import FixedGridPanel from '@/ui/GridPanel/FixedGridPanel.vue';
import HeaderWrapper from '@/ui/Header/HeaderWrapper.vue';
import LeftHeader from '@/ui/Header/LeftHeader.vue';

export default defineComponent({
  components: {
    FixedGridPanel,
    HeaderWrapper,
    LeftHeader,
  },
});
</script>

<style scoped></style>
